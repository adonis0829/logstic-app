/**
 * Date helper methods
 */
export class DateUtils {
  /**
   * Gets a today's date value.
   * @returns 
   */
  today(): Date {
    return new Date();
  }

  /**
   * Gets a day of the specified date.
   * @param dateStr Date value in string format
   */
  getDay(dateStr: string | Date): number {
    return new Date(dateStr).getDay();
  }

  /**
   * Converts a date string to a string by using the current locale.
   * @param date Specify date value either in string or the `Date` object.
   */
  toLocaleDate(date: string | Date): string {
    if (date instanceof Date) {
      return date.toLocaleDateString();
    }

    return new Date(date).toLocaleDateString();
  }

  /**
   * Gets how many days have passed since today
   * @param days A desired number of days to get past date.
   * @returns A new `Date` object of the past date.
   */
  daysAgo(days: number): Date {
    const today = new Date();
    const daysAgo = new Date(today.setDate(today.getDate() - days));
    return daysAgo;
  }
}