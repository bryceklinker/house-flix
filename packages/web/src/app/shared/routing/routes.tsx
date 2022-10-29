import { Navigate, RouteObject } from 'react-router-dom';
import { HouseFlixShell } from '../../shell/HouseFlixShell';
import { WelcomePage } from '../../welcome/WelcomePage';

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <HouseFlixShell />,
    children: [
      {
        index: true,
        element: <WelcomePage />,
      },
      {
        path: '*',
        element: <Navigate to={'/welcome'} replace={true} />,
      },
    ],
  },
];
