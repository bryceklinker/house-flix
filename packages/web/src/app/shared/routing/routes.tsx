import { RouteObject } from 'react-router-dom';
import { HouseFlixShell } from '../../shell/HouseFlixShell';

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <HouseFlixShell />,
  },
];
