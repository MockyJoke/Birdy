import { MyMaterialModuleModule } from './my-material.module';

describe('MyMaterialModuleModule', () => {
  let myMaterialModuleModule: MyMaterialModuleModule;

  beforeEach(() => {
    myMaterialModuleModule = new MyMaterialModuleModule();
  });

  it('should create an instance', () => {
    expect(myMaterialModuleModule).toBeTruthy();
  });
});
