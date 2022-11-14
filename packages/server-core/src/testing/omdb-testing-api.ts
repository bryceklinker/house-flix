import { setupServer } from 'msw/node';
import { rest, ResponseResolver, RestRequest, RestContext } from 'msw';
import {
  OMDBErrorResponse,
  OMDBItemResponse,
  OMDBSearchItem,
  OMDBSearchResponse,
} from '../lib';
import { TESTING_SERVER_CONFIG } from './testing-server-config';

const server = setupServer();

export type RequestOptions = {
  delay?: number;
  status?: number;
  capture?: (req: RestRequest) => unknown;
};

function createRestHandler<T>(
  result: T,
  options?: RequestOptions
): ResponseResolver<RestRequest, RestContext> {
  const status = options?.status || 200;
  const delay = options?.delay || 0;
  const capture = options?.capture;
  return (req, res, ctx) => {
    if (capture) {
      capture(req);
    }
    return res(
      ctx.status(status),
      ctx.delay(delay),
      ctx.text(JSON.stringify(result))
    );
  };
}

function setupSearch(
  items: OMDBSearchItem[],
  options?: RequestOptions & { totalResults?: number }
) {
  server.use(
    rest.get(
      TESTING_SERVER_CONFIG.omdb.url,
      createRestHandler<OMDBSearchResponse>(
        {
          Response: true,
          Search: items,
          totalResults: options?.totalResults || items.length,
        },
        options
      )
    )
  );
}

function setupGetItem(
  result: OMDBItemResponse | OMDBErrorResponse,
  options?: RequestOptions
) {
  server.use(
    rest.get(TESTING_SERVER_CONFIG.omdb.url, createRestHandler(result, options))
  );
}

export const OMDBTestingApi = {
  start: () => server.listen(),
  stop: () => server.close(),
  reset: () => server.resetHandlers(),
  setupSearch,
  setupGetItem,
};
