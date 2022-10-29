import { FC, HTMLAttributes } from 'react';

export type HouseFlixMainProps = HTMLAttributes<HTMLDivElement>;

export const HouseFlixMain: FC<HouseFlixMainProps> = ({
  className,
  ...props
}) => {
  return (
    <main className={`flex flex-1 flex-col p-4 ${className}`} {...props} />
  );
};
