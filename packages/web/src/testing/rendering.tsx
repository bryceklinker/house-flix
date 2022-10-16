import { ReactElement } from 'react';
import { createMemoryRouter, RouterProvider } from 'react-router-dom';
import { render as rtlRender } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { ApolloClient, ApolloProvider, InMemoryCache } from '@apollo/client';

export type RenderOptions = {
  path?: string;
  route?: string;
};
export function render(ui: ReactElement, options?: RenderOptions) {
  const router = createMemoryRouter(
    [
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
export { screen } from '@testing-library/react';
