import { ButtonHTMLAttributes, FC, ReactElement } from 'react';

import './IconButton.css';
export type IconButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  icon: ReactElement;
};

export const IconButton: FC<IconButtonProps> = ({
  className,
  icon,
  children,
  ...props
}) => {
  return (
    <button className={`btn-primary ${className}`} {...props}>
      {icon}
      {children}
    </button>
  );
};
