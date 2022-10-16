import { FC } from 'react';
import { IconButton, RowFlex } from '@house-flix/react-ui';
import { Bars3Icon } from '@heroicons/react/20/solid';

export type HouseFlixHeaderProps = {
  onMenuClick?: () => void;
};
export const HouseFlixHeader: FC<HouseFlixHeaderProps> = ({ onMenuClick }) => {
  return (
    <header>
      <RowFlex>
        <IconButton
          aria-label={'show menu'}
          icon={<Bars3Icon className={'h-20 w-20'} />}
          onClick={onMenuClick}
        />
      </RowFlex>
    </header>
  );
};
