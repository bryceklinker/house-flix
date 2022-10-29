import { render, userEvent, screen } from '../../testing';
import { HouseFlixShell } from './HouseFlixShell';
import { routes } from '../shared/routing/routes';

test('when user wants to see the menu then menu is shown', async () => {
  render(<HouseFlixShell />);

  await userEvent.click(screen.getByRole('button', { name: 'show menu' }));

  expect(screen.getAllByRole('link').length).toBeGreaterThan(0);
});

test('when rendered then shows welcome route', async () => {
  render(<HouseFlixShell />, { routes });

  expect(screen.getByRole('heading')).toHaveTextContent('Welcome');
});
