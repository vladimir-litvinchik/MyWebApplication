import { getGreeting } from '../support/app.po';
describe('viewer', () => {
    beforeEach(() => cy.visit('/'));
    it('should display welcome message', () => {
        getGreeting().contains('Welcome to viewer!');
    });
});
//# sourceMappingURL=app.spec.js.map