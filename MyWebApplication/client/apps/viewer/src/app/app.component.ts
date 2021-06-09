import { Component } from '@angular/core';
import { ViewerAppComponent, ViewerService, ViewerConfigService } from '@groupdocs.examples.angular/viewer';
import {  ModalService, UploadFilesService, NavigateService, ZoomService, PagePreloadService, RenderPrintService, PasswordService, WindowService, LoadingMaskService, PageModel } from '@groupdocs.examples.angular/common-components';

@Component({
  selector: 'client-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less', './variables.less']
})
export class AppComponent extends ViewerAppComponent {

  constructor(viewerService: ViewerService,
    modalService: ModalService,
    viewerConfigService: ViewerConfigService,
    uploadFilesService: UploadFilesService,
    navigateService: NavigateService,
    zoomService: ZoomService,
    pagePreloadService: PagePreloadService,
    renderPrintService: RenderPrintService,
    passwordService: PasswordService,
    windowService: WindowService,
    loadingMaskService: LoadingMaskService) {

    super(viewerService,
      modalService,
      viewerConfigService,
      uploadFilesService,
      navigateService,
      zoomService,
      pagePreloadService,
      renderPrintService,
      passwordService,
      windowService,
      loadingMaskService);
  }
}
