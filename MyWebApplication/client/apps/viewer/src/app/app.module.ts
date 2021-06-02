import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';

import { AppComponent } from './app.component';
import { ViewerModule } from "@groupdocs.examples.angular/viewer";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ConfigService } from '@groupdocs.examples.angular/common-components';

declare global {
  interface Window {
    apiEndpoint: string;
  }
}

export function createViewerConfig() {
  const endpoint = window.apiEndpoint;

  const config = new ConfigService();
  config.apiEndpoint = endpoint;
  config.getViewerApiEndpoint = () => endpoint;
  return config;
}

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, ViewerModule, FontAwesomeModule],
  providers: [
    { provide: APP_BASE_HREF, useValue: '/' },
    { provide: ConfigService, useFactory: createViewerConfig },
    { provide: 'WINDOW', useValue: window },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
