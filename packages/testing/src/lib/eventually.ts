import { addMilliseconds } from 'date-fns';
import { delay } from './delay';

export type EventuallyExpectation<T> = () => Promise<T> | T;

export type EventuallyOptions = {
  timeoutMs: number;
  delayMs: number;
};

export async function eventually<T = unknown>(
  expectation: EventuallyExpectation<T>,
  options?: Partial<EventuallyOptions>
) {
  const timeoutMs = options?.timeoutMs || 4000;
  const delayMs = options?.delayMs || 200;
  const endTime = addMilliseconds(new Date(), timeoutMs);
  let error: Error | null = null;
  while (new Date().getTime() <= endTime.getTime()) {
    try {
      return await expectation();
    } catch (err) {
      await delay(delayMs);
      error = err as Error;
    }
  }

  throw error;
}
