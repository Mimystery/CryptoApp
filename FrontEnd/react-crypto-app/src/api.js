import { useContext } from "react";
import { cryptoWallet } from "./data";
import axios from 'axios';
import { CryptoContext } from "./context/crypto-context";

const symbols = ["BTCUSDT", "ETHUSDT", "SOLUSDT", "BNBUSDT", "XRPUSDT"];
const ids = ['bitcoin', 'ethereum', 'solana', 'binancecoin', 'ripple'];

export const fetchPrice = async () => {
    try 
    {
      const responses = await Promise.all(symbols.map(symbol =>
            fetch(`https://api.binance.com/api/v3/ticker/price?symbol=${symbol}`)
            .then(res => res.json()))
        );
        return responses;
    } 
    catch (error) 
    {
      console.error("Ошибка при получении цены BTC:", error);
    } 
}

export const fetchCryptoWallet = async () =>{
const {user} = useContext(CryptoContext)
  try
  {
    const wallet = await axios.get(`https://cryptoapp-foee.onrender.com/api/Telegram/user/${user?.id}/summary`)
    return wallet.data;
  }
  catch(e)
  { 
    console.error("Ошибка при получении wallet:", e);
  }
}

export const fetchSelectCoins = async (token) => {
  try 
    {
       const token = localStorage.getItem('jwt');
      console.log("Token in method fetchSelectCoins:  " + token)
      const response = await fetch(`https://cryptoapp-foee.onrender.com/api/Coin/list`,{
      method: 'GET',
       //credentials: 'include',
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    if (!response.ok) {
      throw new Error('Ошибка авторизации');
    }

      const data = await response.json(); 
      return data;
    }
    catch (error) 
    {
      console.error("Ошибка при получении списка криптовалют:", error);
      return [];
    } 
}

export const fetchFullCoins = async () => {
  try 
    {
      const matchedCoins = await fetchSelectCoins();
    }
    catch (error) 
    {
      console.error("Ошибка при получении списка криптовалют:", error);
    } 
}