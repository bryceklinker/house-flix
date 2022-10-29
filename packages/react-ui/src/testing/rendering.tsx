import { ReactElement } from 'react';
import { render as rtlRender, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';

export function render(ui: ReactElement) {
  return rtlRender(ui);
}

export { screen, userEvent };
