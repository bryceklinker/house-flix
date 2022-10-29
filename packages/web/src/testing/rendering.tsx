import { ReactElement } from 'react';
import {
  createMemoryRouter,
  RouteObject,
  RouterProvider,
} from 'react-router-dom';
import {
  render as rtlRender,
  waitForElementToBeRemoved,
} from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { ApolloClient, ApolloProvider, InMemoryCache } from '@apollo/client';

export type RenderOptions = {
  path?: string;
  route?: string;
  routes?: RouteObject[];
};
export function render(ui: ReactElement, options?: RenderOptions) {
  const router = createMemoryRouter(
    options?.routes || [
      {
        path: options?.route || '/',
        element: ui,
      },
    ],
    { initialEntries: [options?.path || '/'] }
  );

  const apolloClient = new ApolloClient({
    cache: new InMemoryCache(),
  });

  return rtlRender(
    <ApolloProvider client={apolloClient}>
      <RouterProvider router={router} />
    </ApolloProvider>
  );
}

export { userEvent };
export {
  screen,
  waitForElementToBeRemoved,
  waitFor,
} from '@testing-library/react';
