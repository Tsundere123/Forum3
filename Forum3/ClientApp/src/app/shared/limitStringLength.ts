import { Pipe, PipeTransform } from '@angular/core';
@Pipe({ name: 'limitStringLength' })

export class LimitStringLength implements PipeTransform {
  transform(value: string, length: number): string {
    if (value.length > length) {
      return value.substring(0, length) + "...";
    }
    return value;
  }
}
