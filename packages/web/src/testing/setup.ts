import { TestingApi } from './testing-api';
import { FakeIntersectionObserver } from './fake-intersection-observer';

import 'cross-fetch/polyfill';
import '@testing-library/jest-dom';

beforeAll(() => {
  // eslint-disable-next-line @typescript-eslint/ban-ts-comment
  // @ts-ignore
  global.IntersectionObserver = FakeIntersectionObserver;

  TestingApi.start();
});

beforeEach(() => {
  TestingApi.reset();
});

afterAll(() => {
  TestingApi.stop();
});
