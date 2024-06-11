import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContactComponent } from './pages/contact/contact.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorMessagePipe } from './core/pipe/error-message.pipe';
import { AddEditContactComponent } from './pages/contact/modal/add-edit-contact/add-edit-contact.component';
import { ConfirmationComponent } from './shared/confirmation/confirmation.component';
import { ErrorInterceptor } from './core/interceptor/error.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    ContactComponent,
    AddEditContactComponent,
    ConfirmationComponent,
    ErrorMessagePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide:HTTP_INTERCEPTORS,useClass:ErrorInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
