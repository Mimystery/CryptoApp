export const percentDifference = (a, b) => {
  const change = ((b - a) / a) * 100;
  return change.toFixed(2);
}