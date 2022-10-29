import { setupServer } from 'msw/node';
import {
  graphql,
  ResponseResolver,
  GraphQLRequest,
  GraphQLVariables,
  GraphQLContext,
  ResponseTransformer,
  rest,
  RestRequest,
  RestContext,
} from 'msw';
import { ConfigModel } from '../app/shared/config/config-model';
import { TESTING_CONFIG } from './testing-config';

const server = setupServer();

export type RequestOptions = {
  delay?: number;
  status?: number;
};

function createGraphQLResolver<T>(
  body: T,
  options?: RequestOptions
): ResponseResolver<
  GraphQLRequest<GraphQLVariables>,
  GraphQLContext<Record<string, unknown>>
> {
  return (req, res, ctx) => {
    const transformers: ResponseTransformer[] = [
      ctx.delay(options?.delay || 0),
      ctx.status(options?.status || 200),
    ];
    return res(...transformers);
  };
}

function createRestResolver<T>(
  body: T,
  options?: RequestOptions
): ResponseResolver<RestRequest, RestContext> {
  return (req, res, ctx) => {
    const transformers: ResponseTransformer[] = [
      ctx.delay(options?.delay || 0),
      ctx.status(options?.status || 200),
    ];

    if (body) {
      transformers.push(ctx.json(body));
    }
    return res(...transformers);
  };
}

function setupQuery<T>(name: string, body: T, options?: RequestOptions) {
  server.use(graphql.query(name, createGraphQLResolver(body, options)));
}

function setupGet<T>(url: string, data: T, options?: RequestOptions) {
  server.use(rest.get(url, createRestResolver(data, options)));
}

function setupConfig(config: ConfigModel, options?: RequestOptions) {
  setupGet('/config.json', config, options);
}

function setupDefaults() {
  setupConfig(TESTING_CONFIG);
}

export const TestingApi = {
  start: () => server.listen(),
  stop: () => server.close(),
  reset: () => {
    server.resetHandlers();
    setupDefaults();
  },
  setupQuery,
  setupGet,
  setupConfig,
};
