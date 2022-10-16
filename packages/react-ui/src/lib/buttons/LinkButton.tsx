import { AnchorHTMLAttributes, FC } from 'react';
import { Link } from 'react-router-dom';

export type LinkButtonProps = AnchorHTMLAttributes<HTMLAnchorElement> & {
  to: string;
};

export const LinkButton: FC<LinkButtonProps> = ({ className, ...props }) => {
  return <Link className={`${className}`} {...props} />;
};
