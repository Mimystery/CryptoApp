import { useEffect } from 'react';

export default function TelegramWidget() {
  useEffect(() => {
    // Глобальная функция, которую вызывает Telegram
    window.onTelegramAuth = function (user) {
      console.log(user)

      fetch('https://cryptoapp-foee.onrender.com/api/Telegram/auth', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        credentials: 'include',
        body: JSON.stringify(user),
      })
        .then((res) => res.json())
        .then((data) => {
          console.log('🔵 Ответ от сервера:', data);
          let selectCoins = fetchSelectCoins();
          console.log(selectCoins)
          if (data.token) {
        localStorage.setItem('jwt', data.token);
      } else {
        console.error('⚠️ Токен отсутствует в ответе:', data);
      }
        })
        .catch((err) => {
          console.error('❌ Ошибка отправки:', err);
        });
    };

    // Создаём скрипт Telegram
    const script = document.createElement('script');
    script.src = 'https://telegram.org/js/telegram-widget.js?22';
    script.async = true;
    script.setAttribute('data-telegram-login', 'cryptoappservicebot');
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
