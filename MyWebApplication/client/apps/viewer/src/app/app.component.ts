import { Inject, Component, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ViewerAppComponent, ViewerService, ViewerConfigService } from '@groupdocs.examples.angular/viewer';
import { ConfigService, ModalService, UploadFilesService, NavigateService, ZoomService, PagePreloadService, RenderPrintService, PasswordService, WindowService, LoadingMaskService, PageModel } from '@groupdocs.examples.angular/common-components';

@Component({
  selector: 'client-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less', './variables.less']
})
export class AppComponent extends ViewerAppComponent implements AfterViewInit {

  viewerService: ViewerService;
  pagesLoading: number[];

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
    loadingMaskService: LoadingMaskService,
    @Inject(ConfigService) private config: ConfigService,
    @Inject("WINDOW") private window: Window,
    private cdr: ChangeDetectorRef) {

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

  ngAfterViewInit() {
    const queryString = this.window.location.search;
    if (queryString) {
      const file = this.getParameterByName("f", queryString);
      if (file) {
        this.selectFile(this.viewerConfig.filesDirectory + '\\' + file, '', '');
      }
    }

    this.cdr.detectChanges();
  }

  getParameterByName(name, url) {
    name = name.replace(/[\[\]]/g, '\\$&');
    const regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)');
    const results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';

    return decodeURIComponent(results[2].replace(/\+/g, ' '));
  }
}
