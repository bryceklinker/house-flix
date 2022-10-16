import { NavigationCommands } from '../support/navigation.commands';
import { WelcomePage } from '../support/welcome.po';

describe('web', () => {
  beforeEach(() => NavigationCommands.navigate('/'));

  it('should display welcome message', () => {
    WelcomePage.header().should('have.text', 'Welcome');
  });
});
