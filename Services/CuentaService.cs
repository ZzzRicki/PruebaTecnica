using System;
using System.Collections.Generic;
using PruebaTecnica.Models;

namespace PruebaTecnica.Services
{

    public class CuentaService
    {
        private readonly List<CrearCuenta> _cuentas = new();

        private int _nextCuentaId = 1;

        private int _nextMovimientoId = 1;

        public CrearCuenta CrearCuenta(
            string numeroCuenta,
            string nombreSocio,
            decimal saldoInicial)
        {
            if (saldoInicial < 0)
            {
                throw new ArgumentException(
                    "El saldo inicial no puede ser negativo.");
            }

            var cuenta = new CrearCuenta
            {
                Id = _nextCuentaId++,
                NumeroCuenta = numeroCuenta,
                NombreSocio = nombreSocio,
                SaldoInicial = saldoInicial
            };

            _cuentas.Add(cuenta);

            return cuenta;
        }

        public void Depositar(int cuentaId, decimal monto)
        {
            var cuenta = ObtenerCuenta(cuentaId);

            if (monto <= 0)
            {
                throw new ArgumentException(
                    "El monto debe ser mayor que cero.");
            }

            cuenta.SaldoInicial += monto;

            cuenta.Movimientos.Add(new Movimiento
            {
                Id = _nextMovimientoId++,
                CuentaId = cuenta.Id,
                Monto = monto,
                Tipo = "Deposito",
                Fecha = DateTime.UtcNow
            });
        }

        public void Retirar(int cuentaId, decimal monto)
        {
            var cuenta = ObtenerCuenta(cuentaId);

            if (monto <= 0)
            {
                throw new ArgumentException(
                    "El monto debe ser mayor que cero.");
            }

            if (cuenta.SaldoInicial < monto)
            {
                throw new InvalidOperationException(
                    "Fondos insuficientes.");
            }

            cuenta.SaldoInicial -= monto;

            cuenta.Movimientos.Add(new Movimiento
            {
                Id = _nextMovimientoId++,
                CuentaId = cuenta.Id,
                Monto = -monto,
                Tipo = "Retiro",
                Fecha = DateTime.UtcNow
            });
        }

        public IReadOnlyList<Movimiento> ObtenerHistorial(int cuentaId)
        {
            var cuenta = ObtenerCuenta(cuentaId);

            return cuenta.Movimientos
                .OrderBy(m => m.Fecha)
                .ToList();
        }

        private CrearCuenta ObtenerCuenta(int cuentaId)
        {
            var cuenta = _cuentas
                .FirstOrDefault(c => c.Id == cuentaId);

            if (cuenta is null)
            {
                throw new KeyNotFoundException(
                    $"La cuenta {cuentaId} no existe.");
            }

            return cuenta;
        }
    }
}
