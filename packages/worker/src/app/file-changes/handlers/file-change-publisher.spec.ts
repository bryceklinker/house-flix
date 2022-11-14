import {
  startTestingWorkerApp,
  StartTestingWorkerAppResult,
} from '../../../testing/start-testing-worker-app';

describe('FileChangePublisher', () => {
  let result: StartTestingWorkerAppResult;

  beforeEach(async () => {
    result = await startTestingWorkerApp();
  });

  test('when file changes then notifies event bus', () => {});

  afterEach(async () => {
    await result.cleanup();
  });
});
