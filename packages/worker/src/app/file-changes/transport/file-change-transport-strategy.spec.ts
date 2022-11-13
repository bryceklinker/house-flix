import { Test } from '@nestjs/testing';
import { promises as fs } from 'fs';
import * as path from 'path';
import { v4 as uuid } from 'uuid';
import { FileChangeTransportStrategy } from './file-change-transport-strategy';
import { eventually } from '@house-flix/testing';
import { INestMicroservice, Injectable } from '@nestjs/common';
import { MessagePattern } from '@nestjs/microservices';
import { DirectoryChangeEvent } from './directory-monitor';

const TESTING_DIR = path.resolve(__dirname, `${uuid()}`);

@Injectable()
class FakeSubscriber {
  private static events: DirectoryChangeEvent[] = [];

  public static getEvents(): DirectoryChangeEvent[] {
    return FakeSubscriber.events;
  }

  @MessagePattern<Partial<DirectoryChangeEvent>>({ type: 'add' })
  public handleAdds(event: DirectoryChangeEvent) {
    FakeSubscriber.events = [...FakeSubscriber.events, event];
  }

  public static clearEvents() {
    FakeSubscriber.events = [];
  }
}

describe('FileChangeTransportStrategy', () => {
  let app: INestMicroservice;

  beforeEach(async () => {
    await fs.mkdir(TESTING_DIR);

    const module = await Test.createTestingModule({
      controllers: [FakeSubscriber],
    }).compile();
    app = module.createNestMicroservice({
      strategy: new FileChangeTransportStrategy({
        directory: TESTING_DIR,
      }),
    });
    await app.listen();
  });

  test('when file is created in directory then notifies handler', async () => {
    await fs.writeFile(path.join(TESTING_DIR, 'my-file.txt'), '453', {
      encoding: 'utf-8',
    });

    await eventually(() => {
      expect(FakeSubscriber.getEvents()).toContainEqual<DirectoryChangeEvent>({
        type: 'add',
        isFile: true,
        isDirectory: false,
        path: path.join(TESTING_DIR, 'my-file.txt'),
      });
    });
  });

  afterEach(async () => {
    try {
      await app.close();
      FakeSubscriber.clearEvents();
    } finally {
      await fs.rm(TESTING_DIR, { force: true, recursive: true });
    }
  });
});
