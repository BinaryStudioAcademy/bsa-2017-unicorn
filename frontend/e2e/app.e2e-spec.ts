import { UnicornPage } from './app.po';

describe('unicorn App', () => {
  let page: UnicornPage;

  beforeEach(() => {
    page = new UnicornPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
