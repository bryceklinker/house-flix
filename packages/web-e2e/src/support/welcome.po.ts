const header = () => cy.findByRole('heading', { name: 'welcome message' });

export const WelcomePage = {
  header,
};
