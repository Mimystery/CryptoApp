import { cryptoWallet } from "./data";
import axios from 'axios';

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
  try
  {
    return new Promise((resolve) => {
      setTimeout(() => { 
        resolve(cryptoWallet)
      }, 1)
    })
  }
  catch(e)
  { 
    console.error("Ошибка при получении wallet:", error);
  }
}

export const fetchSelectCoins = async () => {
  try 
    {
      const response = await fetch(`https://cryptoapp-foee.onrender.com/api/Coin/list`)
        .then(res => res.json())

        const fullCoinData = await Promise.all(response); 
        return fullCoinData;
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