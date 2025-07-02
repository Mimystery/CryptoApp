import { Layout, Typography } from 'antd';
import { useContext } from 'react';
import { CryptoContext } from '../../context/crypto-context';
import PortfolioChart from './PortfolioChart';
import TransactionsTable from './TransactionsTable';

const contentStyle = {
  textAlign: 'center',
  height: 'calc(100vh - 60px)',
  color: '#fff',
  backgroundColor: '#f2f5ff',
  padding: '1rem',
};

export default function WalletWidget(){
const {wallet, prices} = useContext(CryptoContext)

const cryptoPriceMap = prices.reduce((acc, c) => {
  acc[c.symbol.replace(/USDT$/, '').toLowerCase()] = +(c.price)
  return acc
}, {})

const portfolio = wallet.map(coin => coin.totalAmount * cryptoPriceMap[coin.symbol.toLowerCase()])
            .reduce((acc, v) => (acc += v), 0).toFixed(2)

    return (
        <Layout.Content>
          <Typography.Title level={3} style={{textAlign: 'left'}}>
            Portfolio: {portfolio}$
          </Typography.Title>
          <PortfolioChart/>
          <TransactionsTable/>
        </Layout.Content>
    )
}