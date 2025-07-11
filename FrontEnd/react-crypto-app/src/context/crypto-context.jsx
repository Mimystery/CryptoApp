import { createContext } from "react";
import { useState, useEffect } from "react";
import { addTransaction, fetchAllTransactions, fetchCryptoWallet, fetchPrice, fetchSelectCoins } from '../api';
import { isTokenExpired, percentDifference } from '../utils'

export const CryptoContext = createContext({
    wallet: [],
    prices: [],
    transactions: [],
    selectCoins: [],
    isAuthenticated: false,
    user: null,
    loading: false,
})

export const CryptoContextProvider = ({children}) => {
const [prices, setPrices] = useState([]);
const [wallet, setWallet] = useState([]);
const [transactions, setTransactions] = useState([]);
const [loading, setLoading] = useState(true);
const [selectCoins, setSelectCoins] = useState([]);
const [isAuthenticated, setIsAuthenticated] = useState(false);
const [user, setUser] = useState(null);
const [isInitialized, setIsInitialized] = useState(false);

console.log(user)

const mapWallet = (wallet, prices) =>{
  if (!Array.isArray(wallet) || !Array.isArray(prices)) return [];
  return wallet.map(walletCoin => {
    const coin = prices.find(c => c.symbol.replace(/USDT$/, "").toLowerCase() === walletCoin.symbol.toLowerCase());
    const currentPrice = parseFloat(coin.price)
    const price = parseFloat(walletCoin.averagePrice);
    const avgPrice = parseFloat(walletCoin.averagePrice);
    const totalAmount = parseFloat(walletCoin.totalAmount);
    const invested = parseFloat(walletCoin.invested)
    const earned = parseFloat(walletCoin.earned)
    const investedOnSold = parseFloat(walletCoin.investedOnSold)

    let grow = null;
    let growPercent = 0;

    if(totalAmount === 0 && invested > 0 && earned > 0){
      grow = earned > invested;
      growPercent = percentDifference(invested, earned);
    }
    else if(totalAmount > 0){
      grow = price < currentPrice;
      growPercent = percentDifference(price, currentPrice)
    }

    return {
      grow: grow,
      growPercent: growPercent,
      totalProfit: totalAmount === 0 ? 
      earned - invested : (earned - investedOnSold) + (currentPrice - avgPrice) * totalAmount,
      totalCost: walletCoin.totalAmount * coin.price,
      ...walletCoin
    };
  })
}

useEffect(() =>{
  const token = localStorage.getItem('jwt');
  const savedUser = localStorage.getItem('userData')
  isTokenExpired(token)
  if(!isTokenExpired(token) && token && savedUser){
    setIsAuthenticated(true);
    setUser(JSON.parse(savedUser))
  }
  else{
    setIsAuthenticated(false);
    setUser(null)
    localStorage.removeItem('jwt');
    localStorage.removeItem('userData')
  }

  setIsInitialized(true)
}, [])

  useEffect(() => {
    
    const fetchCryptoPrice = async () =>{
        let prices = await fetchPrice();
        let wallet = await fetchCryptoWallet();

        setWallet(mapWallet(wallet, prices))
        setPrices(prices)
        setLoading(false)
    }

    fetchCryptoPrice()
    const interval = setInterval(fetchCryptoPrice, 2000); 

    return () => clearInterval(interval); 
  }, []);

  useEffect(() => {
    const token = localStorage.getItem('jwt');
    if (!isInitialized || !isAuthenticated) return;

    const fetchCryptoSelect = async () => {
        setLoading(true)
        let selectCoins = await fetchSelectCoins(token);
        let transactions = await fetchAllTransactions();
        
        setSelectCoins(selectCoins)
        setTransactions(transactions)
        setLoading(false)

    }
    fetchCryptoSelect()
  }, [isAuthenticated, isInitialized]);

  const addCoin = async (newCoin) => {
    let prices = await fetchPrice();
    try
    {
    await addTransaction(newCoin);
    let wallet = await fetchCryptoWallet();
    let transactions = await fetchAllTransactions();

    setWallet(mapWallet(wallet, prices))
    setTransactions(transactions)
    }
    catch(error){
      console.error(error)
    }
  }


    return(
    <CryptoContext.Provider value={{prices, wallet, loading, selectCoins, isAuthenticated, 
    setIsAuthenticated, user, setUser, setLoading, addCoin, transactions, setTransactions}}>
        {children}
    </CryptoContext.Provider>
    )
}