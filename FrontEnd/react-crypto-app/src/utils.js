export const percentDifference = (a, b) => {
  const change = ((b - a) / a) * 100;
  return change.toFixed(2);
}

export const isTokenExpired = (token) => {
    try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        const exp = payload.exp * 1000; // ms
        const now = Date.now(); // тоже в ms

        const secondsLeft = Math.floor((exp - now) / 1000);
        console.log('⏱ Time until token expiration:', secondsLeft, 'seconds');

        return now > exp;
    } catch (e) {
        return true; // если невалидный токен — считаем его просроченным
    }
};