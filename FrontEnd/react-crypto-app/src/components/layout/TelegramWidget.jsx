import { use, useContext, useEffect } from 'react';
import { fetchSelectCoins } from '../../api';
import { CryptoContext } from '../../context/crypto-context';

export default function TelegramWidget() {
const { setIsAuthenticated, setUser, setLoading } = useContext(CryptoContext)

  useEffect(() => {
    window.onTelegramAuth = function (user) {
      setLoading(true)
      console.log(user)
      localStorage.setItem('userData', JSON.stringify(user));
      setUser(user)
      fetch('https://cryptoapp-foee.onrender.com/api/Telegram/auth', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        // credentials: 'include',
        body: JSON.stringify(user),
      })
        .then((res) => res.json())
        .then((data) => {
          console.log('🔵 Ответ от сервера:', data);
          console.log(user)
          
      if (data.token) 
      {
      localStorage.setItem('jwt', data.token);
      setIsAuthenticated(true);
      window.location.reload();
      } 
      else 
      {
        console.error('⚠️ Токен отсутствует в ответе:', data);
      }
      })
        .catch((err) => {
          console.error('❌ Ошибка отправки:', err);
        }).finally(() => {
          setLoading(false);
        });
    };

    const script = document.createElement('script');
    script.src = 'https://telegram.org/js/telegram-widget.js?22';
    script.async = true;
    script.setAttribute('data-telegram-login', 'cryptoappauthbot');
    script.setAttribute('data-size', 'medium');
    script.setAttribute('data-radius', '10');
    script.setAttribute('data-request-access', 'write');
    script.setAttribute('data-onauth', 'onTelegramAuth(user)');

    const container = document.getElementById('telegram-login');
    if (container) {
      container.innerHTML = '';
      container.appendChild(script);
    }

    return () => {
      delete window.onTelegramAuth;
      if (container) container.innerHTML = '';
    };
  }, []);

  return (
    <div
      id="telegram-login"
      style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50px' }}
    />
  );
}
