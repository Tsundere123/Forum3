import { ConvertToReadableDate } from "./convert-to-readable-date";

describe('ConvertToReadableDate', () => {
  it('create an instance', () => {
    const pipe = new ConvertToReadableDate();
    expect(pipe).toBeTruthy();
  });
});
