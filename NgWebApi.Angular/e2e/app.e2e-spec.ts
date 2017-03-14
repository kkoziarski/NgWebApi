import { NgWebAppPage } from './app.po';

describe('ng-web-app App', () => {
  let page: NgWebAppPage;

  beforeEach(() => {
    page = new NgWebAppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
