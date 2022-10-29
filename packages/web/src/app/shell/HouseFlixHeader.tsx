import { FC } from 'react';
import { IconButton, RowFlex } from '@house-flix/react-ui';
import { Bars3Icon } from '@heroicons/react/20/solid';

export type HouseFlixHeaderProps = {
  onMenuClick?: () => void;
};
export const HouseFlixHeader: FC<HouseFlixHeaderProps> = ({ onMenuClick }) => {
  return (
    <header className={'flex w-screen h-20 bg-purple-700 p-2'}>
      <RowFlex>
        <IconButton
          aria-label={'show menu'}
          icon={<Bars3Icon className={'h-8 w-8'} />}
          onClick={onMenuClick}
        />
      </RowFlex>
    </header>
  );
};
