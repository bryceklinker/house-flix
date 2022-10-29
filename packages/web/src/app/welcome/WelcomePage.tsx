import { FC } from 'react';
import { ColumnFlex } from '@house-flix/react-ui';

export const WelcomePage: FC = () => {
  return (
    <ColumnFlex>
      <h1 className={'text-white'}>Welcome to HouseFlix!</h1>
    </ColumnFlex>
  );
};
