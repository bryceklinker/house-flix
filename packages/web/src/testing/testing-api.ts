import { setupServer } from 'msw/node';
import {
  graphql,
  ResponseResolver,
  GraphQLRequest,
  GraphQLVariables,
  GraphQLContext,
  ResponseTransformer,
} from 'msw';

const server = setupServer();

export type GraphQLRequestOptions = {
  delay?: number;
  status?: number;
};

function createGraphQLResolver<T>(
  body: T,
  options?: GraphQLRequestOptions
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

function setupQuery<T>(name: string, body: T, options?: GraphQLRequestOptions) {
  server.use(graphql.query(name, createGraphQLResolver(body, options)));
}

export const TestingApi = {
  start: () => server.listen(),
  stop: () => server.close(),
  reset: () => server.resetHandlers(),
  setupQuery,
};
