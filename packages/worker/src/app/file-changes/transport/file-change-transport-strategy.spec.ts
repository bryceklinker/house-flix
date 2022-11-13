import { promises as fs } from 'fs';
import * as path from 'path';
import { eventually } from '@house-flix/testing';
import { DirectoryChangeEvent } from './directory-monitor';
import { FakeSubscriber } from '../../../testing/fake-subscriber.service';
import {
  startTestingWorkerApp,
  StartTestingWorkerAppResult,
} from '../../../testing/start-testing-worker-app';

describe('FileChangeTransportStrategy', () => {
  let result: StartTestingWorkerAppResult;

  beforeEach(async () => {
    result = await startTestingWorkerApp();
  });

  test('when file is created in directory then notifies handler', async () => {
    await fs.writeFile(path.join(result.directory, 'my-file.txt'), '453', {
      encoding: 'utf-8',
    });

    await eventually(() => {
      expect(FakeSubscriber.getEvents()).toContainEqual<DirectoryChangeEvent>({
        type: 'add',
        isFile: true,
        isDirectory: false,
        path: path.join(result.directory, 'my-file.txt'),
      });
    });
  });

  afterEach(async () => {
    await result.cleanup();
  });
});
