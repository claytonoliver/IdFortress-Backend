﻿using IdFortress.Application.Services.Contracts;
using IdFortress.Communication.Dto;
using IdFortress.Communication.Dto.Request;
using IdFortress.Communication.Dto.Response;
using IdFortress.Domain.Entitites;
using IdFortress.Infrastructure.Context;
using IdFortress.Infrastructure.Repositories;
using IdFortress.Infrastructure.Repositories.Interface;
using MongoDB.Driver;

namespace IdFortress.Application.Services;

public class BiometriaFacialService : MongoDbGenericRepository<Validacao>, IBiometriaFacialService
{
    private readonly IMongoCollection<Validacao> _validacoes;
    private readonly INotificacaoFraudeService _notificacaoService;

    public BiometriaFacialService(
        IMongoDatabase database,
        MongoDbContext context,
        INotificacaoFraudeService notificacaoService)
        : base(database, "validacoes")
    {
        _validacoes = context.GetCollection<Validacao>("IFT_validacoes");
        _notificacaoService = notificacaoService;
    }

    public async Task<RespostaValidacao> ValidarAsync(RequisicaoBiometriaFacialDto request)
    {
        // Validação básica simulada
        var tipoFraude = DetectarFraude(request.ImagemBase64);
        var fraudeDetectada = tipoFraude != null;

        var entidade = new Validacao
        {
            TransacaoId = request.TransacaoId,
            TipoValidacao = "facial",
            ImagemBase64 = request.ImagemBase64,
            DataCaptura = request.DataCaptura,
            Dispositivo = new Dispositivo
            {
                Fabricante = request.Dispositivo.Fabricante,
                Modelo = request.Dispositivo.Modelo,
                SistemaOperacional = request.Dispositivo.SistemaOperacional
            },
            Metadados = new Metadados
            {
                Latitude = request.Metadados.Latitude,
                Longitude = request.Metadados.Longitude,
                IpOrigem = request.Metadados.IpOrigem
            },
            Sucesso = !fraudeDetectada,
            FraudeDetectada = fraudeDetectada,
            TipoFraude = tipoFraude
        };

        await _validacoes.InsertOneAsync(entidade);

        if (fraudeDetectada)
        {
            await _notificacaoService.NotificarFraudeAsync(new NotificacaoFraudeDto
            {
                TransacaoId = request.TransacaoId,
                TipoBiometria = "facial",
                TipoFraude = tipoFraude,
                DataCaptura = request.DataCaptura,
                Dispositivo = new IdFortress.Communication.Dto.Shared.Dispositivo
                {
                    Fabricante = request.Dispositivo.Fabricante,
                    Modelo = request.Dispositivo.Modelo,
                    SistemaOperacional = request.Dispositivo.SistemaOperacional
                },
                CanalNotificacao = new[] { "sms", "email" },
                NotificadoPor = "sistema-de-monitoramento",
                Metadados = new IdFortress.Communication.Dto.Shared.Metadados
                {
                    Latitude = request.Metadados.Latitude,
                    Longitude = request.Metadados.Longitude,
                    IpOrigem = request.Metadados.IpOrigem
                }
            });

        }

        return new RespostaValidacao
        {
            Sucesso = !fraudeDetectada,
            FraudeDetectada = fraudeDetectada,
            TipoFraude = tipoFraude,
            Mensagem = fraudeDetectada ? "Fraude detectada." : "Validação facial realizada com sucesso."
        };
    }

    private string? DetectarFraude(string imagemBase64)
    {
        // Simulação de fraude com base em alguma regra simples (placeholder)
        if (imagemBase64.Contains("fake")) return "deepfake";
        if (imagemBase64.Contains("mascara")) return "mascara";
        if (imagemBase64.Contains("fotodefoto")) return "foto de foto";
        return null;
    }
}