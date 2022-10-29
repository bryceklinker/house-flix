import { FC, useCallback, useState } from 'react';
import { ColumnFlex } from '@house-flix/react-ui';
import { Outlet } from 'react-router-dom';
import { HouseFlixHeader } from './HouseFlixHeader';
import { HouseFlixSideBar } from './HouseFlixSideBar';
import { HouseFlixMain } from './HouseFlixMain';

export const HouseFlixShell: FC = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const handleOpenMenu = useCallback(
    () => setIsMenuOpen(true),
    [setIsMenuOpen]
  );
  const handleCloseMenu = useCallback(
    () => setIsMenuOpen(false),
    [setIsMenuOpen]
  );
  return (
    <ColumnFlex className={'flex-1 bg-slate-900'}>
      <HouseFlixHeader onMenuClick={handleOpenMenu} />
      <HouseFlixSideBar open={isMenuOpen} onClose={handleCloseMenu} />
      <HouseFlixMain>
        <Outlet />
      </HouseFlixMain>
    </ColumnFlex>
  );
};
