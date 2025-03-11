export const formatHireDate = (hireDate: string) => {
  const date = new Date(hireDate);

  const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: 'long', day: 'numeric' };
  return date.toLocaleDateString('en-US', options);
};
