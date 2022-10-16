import { FC, HTMLAttributes } from 'react';

export type FlexProps = HTMLAttributes<HTMLDivElement>;

export const Flex: FC<FlexProps> = ({ className, ...props }) => {
  return <div className={`flex ${className}`} {...props} />;
};

export const RowFlex: FC<FlexProps> = ({ className, ...props }) => {
  return <Flex className={`flex-row ${className}`} {...props} />;
};

export const ColumnFlex: FC<FlexProps> = ({ className, ...props }) => {
  return <Flex className={`flex-col ${className}`} {...props} />;
};
