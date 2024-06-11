import { pipe } from 'rxjs';
import { ErrorMessagePipe } from './error-message.pipe';
import { ErrorMessage } from '../const/errorMesssage';

describe('ErrorMessagePipe', () => {

  let pipe:ErrorMessagePipe;

  beforeEach(() => {
    pipe = new ErrorMessagePipe();
  })

  it('create an instance', () => {
    expect(pipe).toBeTruthy();
  });

  it('should return the correct error message for a valid key', () => {
    let result:string = pipe.transform('FirstNameRequired');
    expect(result).toEqual(ErrorMessage['FirstNameRequired'])

  });

  


});
