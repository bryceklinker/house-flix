import { FC, HTMLAttributes } from 'react';
import { ColumnFlex } from '../layout/Flex';
import { LoadingSpinner } from './LoadingSpinner';

export type LoadingOverlayProps = HTMLAttributes<HTMLDivElement>;
export const LoadingOverlay: FC<LoadingOverlayProps> = ({
  className,
  children,
  ...props
}) => {
  return (
    <ColumnFlex
      role={'status'}
      className={`fixed z-50 w-0 h-0 justify-center items-center bg-gray-700 ${className}`}
      {...props}
    >
      <LoadingSpinner size={'large'} />
      {children && <h3>{children}</h3>}
    </ColumnFlex>
  );
};
