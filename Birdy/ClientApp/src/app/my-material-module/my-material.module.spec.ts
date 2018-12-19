import { MyMaterialModule } from './my-material.module';

describe('MyMaterialModuleModule', () => {
  let myMaterialModuleModule: MyMaterialModule;

  beforeEach(() => {
    myMaterialModuleModule = new MyMaterialModule();
  });

  it('should create an instance', () => {
    expect(myMaterialModuleModule).toBeTruthy();
  });
});
