export const calculateEmploymentDuration = (hireDate: string) => {
  const hire = new Date(hireDate);
  const now = new Date();

  let years = now.getFullYear() - hire.getFullYear();
  let months = now.getMonth() - hire.getMonth();
  let days = now.getDate() - hire.getDate();

  if (months < 0) {
    years--;
    months += 12;
  }

  if (days < 0) {
    months--;
    const previousMonth = new Date(now.getFullYear(), now.getMonth(), 0);
    days += previousMonth.getDate();
  }

  return { years, months, days };
};
