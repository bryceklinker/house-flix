import * as path from 'path';
import { promises as fs } from 'fs';
import { DirectoryChangeEvent, DirectoryMonitor } from './directory-monitor';
import { delay, eventually } from '@house-flix/testing';

const TESTING_DIRECTORY = path.resolve(__dirname, 'tmp');
describe('DirectoryMonitor', () => {
  let client: DirectoryMonitor;

  beforeEach(async () => {
    await fs.mkdir(TESTING_DIRECTORY);

    client = new DirectoryMonitor(TESTING_DIRECTORY);
    await client.start();
  });

  test('when file added then notifies subscriber', async () => {
    let actual: DirectoryChangeEvent | null = null;
    await client.subscribe((change) => (actual = change));

    await fs.writeFile(path.resolve(TESTING_DIRECTORY, 'my-file.txt'), '123', {
      encoding: 'utf-8',
    });

    await eventually(() =>
      expect(actual).toEqual<DirectoryChangeEvent>({
        path: path.resolve(TESTING_DIRECTORY, 'my-file.txt'),
        type: 'add',
        isFile: true,
        isDirectory: false,
      })
    );
  });

  test('when file deleted then notifies subscriber', async () => {
    let actual: DirectoryChangeEvent | null = null;
    await client.subscribe((change) => (actual = change));

    await fs.writeFile(path.resolve(TESTING_DIRECTORY, 'my-file.txt'), '123', {
      encoding: 'utf-8',
    });
    await delay(200);
    await fs.unlink(path.resolve(TESTING_DIRECTORY, 'my-file.txt'));

    await eventually(() =>
      expect(actual).toEqual<DirectoryChangeEvent>({
        path: path.resolve(TESTING_DIRECTORY, 'my-file.txt'),
        type: 'delete',
        isFile: false,
        isDirectory: false,
      })
    );
  });

  test('when file changed then notifies subscriber', async () => {
    let actual: DirectoryChangeEvent | null = null;
    await client.subscribe((change) => (actual = change));

    await fs.writeFile(path.resolve(TESTING_DIRECTORY, 'my-file.txt'), '123', {
      encoding: 'utf-8',
    });
    await fs.appendFile(path.resolve(TESTING_DIRECTORY, 'my-file.txt'), '456', {
      encoding: 'utf-8',
    });

    await eventually(() =>
      expect(actual).toEqual<DirectoryChangeEvent>({
        path: path.resolve(TESTING_DIRECTORY, 'my-file.txt'),
        type: 'change',
        isFile: true,
        isDirectory: false,
      })
    );
  });

  afterEach(async () => {
    await client.close();
    await fs.rm(TESTING_DIRECTORY, { recursive: true, force: true });
  });
});
