import { Inject, Component, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ViewerAppComponent, ViewerService, ViewerConfigService } from '@groupdocs.examples.angular/viewer';
import { ConfigService, ModalService, UploadFilesService, NavigateService, ZoomService, PagePreloadService, RenderPrintService, PasswordService, WindowService, LoadingMaskService, PageModel } from '@groupdocs.examples.angular/common-components';

type IFrame = HTMLElement & { contentWindow: Window }

@Component({
  selector: 'client-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less', './variables.less']
})
export class AppComponent extends ViewerAppComponent implements AfterViewInit {

  viewerService: ViewerService;
  renderPrintService: RenderPrintService; 
  pagesLoading: number[];

  constructor(
    viewerService: ViewerService,
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

    this.viewerService = viewerService;
    this.renderPrintService = renderPrintService;
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

  doPrint(iframe: IFrame) {
    try {
      iframe.focus()
      iframe.contentWindow.document.execCommand('print', false)
    } catch (e) {
      iframe.contentWindow.print()
    } finally {
      // Make invisible
      iframe.style.visibility = 'hidden'
      iframe.style.left = '-1px'
    }
  }

  printFile() {
    if (this.formatDisabled)
      return;

    this.viewerService.loadPrintPdf(this.credentials).subscribe((blob: Blob) => {
      const iframeId = 'print-window';
      const pdf = window.URL.createObjectURL(blob)

      // Remove previous iframe if exists
      let iframe = document.getElementById(iframeId)
      if (iframe) {
        iframe.remove()
      }

      // Add new iframe
      iframe = document.createElement('iframe')
      iframe.setAttribute('style', 'visibility: hidden; height: 0; width: 0; position: absolute; border: 0');
      iframe.setAttribute('id', iframeId)
      iframe.setAttribute('src', pdf)

      // Append to the document
      document.getElementsByTagName('body')[0].appendChild(iframe);

      // Wait and print
      const iframeElement = document.getElementById(iframeId) as IFrame;
      setTimeout(() => this.doPrint(iframeElement), 1000);
    });
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
