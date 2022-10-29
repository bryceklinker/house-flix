import { AnchorHTMLAttributes, FC } from 'react';
import { NavLink } from 'react-router-dom';

export type LinkButtonProps = AnchorHTMLAttributes<HTMLAnchorElement> & {
  to: string;
};

export const LinkButton: FC<LinkButtonProps> = ({ className, ...props }) => {
  return (
    <NavLink
      className={({ isActive }) =>
        isActive ? `btn-link active ${className}` : `btn-link ${className}`
      }
      {...props}
    />
  );
};
