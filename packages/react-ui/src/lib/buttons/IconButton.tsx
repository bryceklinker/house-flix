import { ButtonHTMLAttributes, FC, ReactElement } from 'react';

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
    <button className={`btn ${className}`} {...props}>
      {icon}
      {children}
    </button>
  );
};
