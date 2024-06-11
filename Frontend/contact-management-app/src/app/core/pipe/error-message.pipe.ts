import { Pipe, PipeTransform } from '@angular/core';
import { ErrorMessage } from '../const/errorMesssage';

@Pipe({
  name: 'errorMessage'
})
export class ErrorMessagePipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): string {
    return ErrorMessage[value as keyof typeof ErrorMessage] || "";
  }

}
