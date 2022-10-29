import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';

import { routes } from './app/shared/routing/routes';
import { ConfigProvider } from './app/shared/config/config-hooks';

const router = createBrowserRouter(routes);
const root = createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <StrictMode>
    <ConfigProvider configUrl={'/assets/config.json'}>
      <RouterProvider router={router} />
    </ConfigProvider>
  </StrictMode>
);
