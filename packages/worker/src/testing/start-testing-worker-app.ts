import { promises as fs } from 'fs';
import { v4 as uuid } from 'uuid';
import * as path from 'path';
import { Test } from '@nestjs/testing';
import { AppModule } from '../app/app.module';
import { FakeSubscriber } from './fake-subscriber.service';
import { FileChangeTransportStrategy } from '../app/file-changes/transport/file-change-transport-strategy';
import { INestMicroservice } from '@nestjs/common';

export type StartTestingWorkerAppResult = {
  app: INestMicroservice;
  directory: string;
  cleanup: () => Promise<void>;
};

export async function startTestingWorkerApp(): Promise<StartTestingWorkerAppResult> {
  const directory = path.resolve(__dirname, uuid());
  await fs.mkdir(directory);

  const module = await Test.createTestingModule({
    imports: [AppModule],
    controllers: [FakeSubscriber],
  }).compile();

  const app = module.createNestMicroservice({
    strategy: new FileChangeTransportStrategy({
      directory,
    }),
  });
  await app.listen();
  return {
    app,
    directory,
    cleanup: async () => {
      try {
        await app.close();
        FakeSubscriber.clearEvents();
      } finally {
        await fs.rm(directory, { force: true, recursive: true });
      }
    },
  };
}
