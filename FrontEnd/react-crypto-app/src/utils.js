export const percentDifference = (a, b) => {
  const change = ((b - a) / a) * 100;
  return change.toFixed(2);
}

export const isTokenExpired = (token) => {
    try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        const exp = payload.exp * 1000; 
        const now = Date.now(); 

        const secondsLeft = Math.floor((exp - now) / 1000);

        return now > exp;
    } catch (e) {
        return true; 
    }
};