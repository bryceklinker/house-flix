import { OMDBTestingApi } from './omdb-testing-api';

beforeAll(() => {
  OMDBTestingApi.start();
});

beforeEach(() => {
  OMDBTestingApi.reset();
});

afterAll(() => {
  OMDBTestingApi.stop();
});
