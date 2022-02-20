using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Diagram.Models;
using FBA.Database.Contract.Diagram.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBA.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/diagram")]
    public class DiagramController : ControllerBase
    {
        private readonly IDiagramService _diagramService;

        public DiagramController(IDiagramService diagramService)
        {
            _diagramService = diagramService;
        }

        [HttpGet("{id}")]
        public async Task<DiagramDocument> Get(string id) => await _diagramService.Get(id); 

        [HttpGet("connection/{id}")]
        public async Task<DiagramDocument> GetByConnection(string id) => await _diagramService.GetByConnection(id); 
    }
}